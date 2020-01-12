using System.Threading.Tasks;

namespace UniversalIRC.Activation
{
    // For more information on application activation see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/activation.md
    internal abstract class ActivationHandler
    {
        public abstract bool CanHandle(object args);

        public abstract Task HandleAsync(object args);
    }

    internal abstract class ActivationHandler<TEventArgs> : ActivationHandler
        where TEventArgs : class
    {
        protected abstract Task HandleInternalAsync(TEventArgs args);

        public override async Task HandleAsync(object args)
        {
            await HandleInternalAsync(args as TEventArgs);
        }

        public override bool CanHandle(object args)
        {
            return args is TEventArgs && CanHandleInternal(args as TEventArgs);
        }

        protected virtual bool CanHandleInternal(TEventArgs args)
        {
            return true;
        }
    }
}
