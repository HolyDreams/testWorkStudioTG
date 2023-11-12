namespace testWorkStudioTG.Models
{
    public class GameStruct : GameInfoResponse
    {
        public byte[][] FillBoard { get; set; }
        public byte[][] Mines { get; set; }
    }
}
