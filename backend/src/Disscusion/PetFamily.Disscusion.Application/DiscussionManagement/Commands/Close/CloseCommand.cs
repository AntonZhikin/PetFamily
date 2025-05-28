using System.Windows.Input;
using ICommand = PetFamily.Core.Abstractions.ICommand;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Commands.Close;

public record CloseCommand(Guid DiscussiondId) : ICommand;
