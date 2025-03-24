namespace BookSwap.DTOs
{
    public class TradeRequestDto
    {
        public int ReceiverId { get; set; }
        public int SenderBookId { get; set; }
        public int ReceiverBookId { get; set; }
    }
}
