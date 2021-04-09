namespace CQRS.Mediatr.Lite
{
    public interface IGenericSearchQuery
    {
        string SearchText { get; set; }
        string Filter { get; set; }
        string Select { get; set; }
        string OrderBy { get; set; }
        int Top { get; set; }
        int Skip { get; set; }
        bool IncludeTotalCount { get; set; }
    }
}
