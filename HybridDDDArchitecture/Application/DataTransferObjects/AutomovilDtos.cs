namespace Application.DataTransferObjects
{
    /// <summary>
    /// DTO de creación de Automóvil.
    /// Usado en POST /api/v1/automovil
    /// </summary>
    public record AutomovilCreateDto(
        string Marca,
        string Modelo,
        string Color,
        int Fabricacion,
        string NumeroMotor,
        string NumeroChasis
    );

    /// <summary>
    /// DTO de actualización de Automóvil.
    /// Usado en PUT /api/v1/automovil/{id}
    /// </summary>
    public record AutomovilUpdateDto(
        string? Color,
        string? NumeroMotor,
        string? NumeroChasis,
        string? Marca,
        string? Modelo,
        int? Fabricacion
    );

    /// <summary>
    /// DTO de resultado (lo que devolvemos al cliente).
    /// Usado en GET/POST/PUT
    /// </summary>
    public record AutomovilResultDto(
        int Id,
        string Marca,
        string Modelo,
        string Color,
        int Fabricacion,
        string NumeroMotor,
        string NumeroChasis
    );
}
