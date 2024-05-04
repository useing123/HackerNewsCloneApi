namespace HackerNewsCloneApi.Models.DTOs
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public int PostId { get; set; }
    }
}
