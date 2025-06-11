namespace DeliverySystemTask.DTOS
{
    public class DeliveryDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool GreenSlot { get; set; }
    }
}
