public class Boat : RealviewObject
{
    public void Interaction(TimelineManager timeline_manager)
    {
        timeline_manager.StartTrailer(TimelineCode.EPILOGUE);
    }
}