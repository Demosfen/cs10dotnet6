namespace WMS.Services.Models.Serialization;

public class PaletteModel
{
        public PaletteModel(
            List<BoxModel> boxes, 
            int volume, 
            int weight, 
            string expiryDate, 
            string id, 
            int width, 
            int height, 
            int depth)
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
        public int Volume { get; }
        public int Weight { get; }
        public string ExpiryDate { get; }
        public string Id { get; }
        public int Width { get; }
        public int Height { get; }
        public int Depth { get; }
}