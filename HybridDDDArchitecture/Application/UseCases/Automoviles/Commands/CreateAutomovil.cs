using Application.DataTransferObjects;

namespace Application.UseCases.Automoviles.Commands
{
    public record CreateAutomovilCommand(AutomovilCreateDto Dto);
}