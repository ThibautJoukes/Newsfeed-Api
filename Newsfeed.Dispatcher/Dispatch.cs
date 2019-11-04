using Newsfeed.Application.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Newsfeed.Dispatcher
{
    public class Dispatch : IDispatcher
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();

        public Dispatch(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // checks if object implements an IRequest
            var requestType = request.GetType();
            var requestInterfaceType = requestType
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>));

            var isValidRequest = requestInterfaceType != null;

            if (!isValidRequest)
            {
                throw new ArgumentException($"{nameof(request)} does not implement IRequest");
            }

            // viewmodel
            var responseType = requestInterfaceType.GetGenericArguments()[0];

            // create instance of the handler that belongs to the request  
            var handler = _requestHandlers.GetOrAdd(requestType,
                 t => Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<,>)
                                .MakeGenericType(requestType, responseType)));

            // call via dynamic dispatch to avoid calling through reflection for performance reasons
            return ((RequestHandlerBase)handler).Handle(request, cancellationToken, _serviceFactory);
        }
    }
}
