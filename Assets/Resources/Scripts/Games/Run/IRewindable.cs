namespace Assets.Resources.Scripts.Games.Run
{
    interface IRewindable
    {
        bool OnHold { get; }

        bool Recording { get; set; }

        void SubscribeRewindable();

        void UnscribleRewindable();

        void Rewind();

        void Record();
    }
}
