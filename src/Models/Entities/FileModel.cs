using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Domain.Entities;


public class FileModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public required string FileName { get; set; }
    public required string FileExtension { get; set; }
    public required string FilePath { get; set; }
    public required long FileSize { get; set; }
}