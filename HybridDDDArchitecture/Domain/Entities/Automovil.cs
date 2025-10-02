namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio: Automóvil
    /// Reglas mínimas:
    /// - Campos requeridos (Marca, Modelo, Color, Fabricacion, NumeroMotor, NumeroChasis)
    /// - Fabricacion: año válido (1900..año actual)
    /// - La unicidad de NumeroMotor y NumeroChasis se valida/garantiza fuera (Application/Infra)
    /// </summary>
    public class Automovil
    {
        public int Id { get; private set; }
        public string Marca { get; private set; } = default!;
        public string Modelo { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public int Fabricacion { get; private set; }
        public string NumeroMotor { get; private set; } = default!;
        public string NumeroChasis { get; private set; } = default!;

        // Requerido por EF
        private Automovil() { }

        public Automovil(string marca, string modelo, string color, int fabricacion,
                         string numeroMotor, string numeroChasis)
        {
            SetMarca(marca);
            SetModelo(modelo);
            SetColor(color);
            SetFabricacion(fabricacion);
            SetNumeroMotor(numeroMotor);
            SetNumeroChasis(numeroChasis);
        }

        /// <summary>
        /// Actualización controlada (PUT total o actualización parcial).
        /// Los parámetros son opcionales; si vienen null o vacíos, no se tocan.
        /// </summary>
        public void Update(
            string? color = null,
            string? numeroMotor = null,
            string? numeroChasis = null,
            string? marca = null,
            string? modelo = null,
            int? fabricacion = null)
        {
            if (!string.IsNullOrWhiteSpace(color)) SetColor(color);
            if (!string.IsNullOrWhiteSpace(numeroMotor)) SetNumeroMotor(numeroMotor);
            if (!string.IsNullOrWhiteSpace(numeroChasis)) SetNumeroChasis(numeroChasis);
            if (!string.IsNullOrWhiteSpace(marca)) SetMarca(marca);
            if (!string.IsNullOrWhiteSpace(modelo)) SetModelo(modelo);
            if (fabricacion.HasValue) SetFabricacion(fabricacion.Value);
        }

        
    }
}