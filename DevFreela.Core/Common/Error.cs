using System.Runtime.InteropServices.JavaScript;
using DevFreela.Core.Enums;

namespace DevFreela.Core.Common;

public sealed class Error
{
    public string Code { get; } // Ex: "User.NotFound", "Skill.NotFound"
    public string Description { get; }
    public ErrorType Type { get; } // Enum para categorizar o erro

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Error NotFound(string code, string description) 
        => new(code, description, ErrorType.NotFound);
    
    public static Error Validation(string code, string description) 
        => new(code, description, ErrorType.Validation);
    
    public static Error Conflict(string code, string description) 
        => new(code, description, ErrorType.Conflict);
}