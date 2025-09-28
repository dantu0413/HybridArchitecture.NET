using Application.DataTransferObjects;

namespace Application.UseCases.Automoviles.Commands
{
    public record UpdateAutomovilCommand(int Id, AutomovilUpdateDto Dto);
}
