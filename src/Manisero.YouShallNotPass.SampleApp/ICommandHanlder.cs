namespace Manisero.YouShallNotPass.SampleApp
{
    public interface ICommandHanlder<TCommand>
    {
        void Handle(TCommand command);
    }
}
