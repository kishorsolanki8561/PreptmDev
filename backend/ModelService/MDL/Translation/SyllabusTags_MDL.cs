namespace ModelService.MDL.Translation
{
    public class SyllabusTags_MDL
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int SyllabusId { get; set; }
        public Syllabus_MDL Syllabus { get; set; }
    }
}
