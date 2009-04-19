namespace Snacks_R_Us.Domain.DataTransfer
{
    public class SnackDto : ISelectListItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ScreenName { get; set; }
        public double Price { get; set; }

        public string Value {get { return Id; }}
        public string Text {get { return ScreenName; }}
    }
}