namespace Snacks_R_Us.Domain.DataTransfer
{
    public class ViewOrderDto
    {
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public double Qty { get; set; }
        public string Snack { get; set; }
    }
}