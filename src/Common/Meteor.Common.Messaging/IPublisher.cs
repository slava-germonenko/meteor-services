namespace Meteor.Common.Messaging;

public interface IPublisher<TData>
{
    public void Publish(TData body);
}