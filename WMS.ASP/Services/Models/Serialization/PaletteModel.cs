namespace WMS.ASP.Services.Models.Serialization;

public class PaletteModel
{
        public PaletteModel(
            List<BoxModel> boxes, 
            decimal volume, 
            decimal weight, 
            DateTime expiryDate, 
            Guid id, 
            decimal width, 
            decimal height, 
            decimal depth)
        {
            Boxes = boxes;
            Volume = volume;
            Weight = weight;
            ExpiryDate = expiryDate;
            Id = id;
            Width = width;
            Height = height;
            Depth = depth;
        }

        public List<BoxModel> Boxes { get; }
        public decimal Volume { get; }
        public decimal Weight { get; }
        public DateTime ExpiryDate { get; }
        public Guid Id { get; }
        public decimal Width { get; }
        public decimal Height { get; }
        public decimal Depth { get; }
}