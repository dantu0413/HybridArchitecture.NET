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
        void SetMarca(string v)
        {
            if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException("Marca requerida", nameof(Marca));
            Marca = v.Trim();
        }

        void SetModelo(string v)
        {
            if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException("Modelo requerido", nameof(Modelo));
            Modelo = v.Trim();
        }

        void SetColor(string v)
        {
            if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException("Color requerido", nameof(Color));
            Color = v.Trim();
        }

        void SetFabricacion(int y)
        {
            int now = DateTime.UtcNow.Year;
            if (y < 1900 || y > now) throw new ArgumentOutOfRangeException(nameof(Fabricacion), "Año de fabricación inválido");
            Fabricacion = y;
        }

        void SetNumeroMotor(string v)
        {

            if (string.IsNullOrWhiteSpace(v))
                throw new ArgumentException("Numero de motor requerido", nameof(NumeroMotor));

            var trimmed = v.Trim();

            if (trimmed.Length != 17)
                throw new ArgumentException("El número de motor debe tener exactamente 17 caracteres.", nameof(NumeroMotor));

            if (!trimmed.All(char.IsLetterOrDigit))
                throw new ArgumentException("El número de motor debe ser alfanumérico.", nameof(NumeroMotor));

            NumeroMotor = trimmed;

        }

        void SetNumeroChasis(string v)
        {
            if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException("Numero de chasis requerido", nameof(NumeroChasis));
            NumeroChasis = v.Trim();
        }


    }

    void SetMarca(string v)
    {
        if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException("Marca requerida", nameof(Marca));
        Marca = v.Trim();
    }

    void SetModelo(string v)
    {
        if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException("Modelo requerido", nameof(Modelo));
        Modelo = v.Trim();
    }

    void SetColor(string v)
    {
        if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException("Color requerido", nameof(Color));
        Color = v.Trim();
    }

    void SetFabricacion(int y)
    {
        int now = DateTime.UtcNow.Year;
        if (y < 1900 || y > now) throw new ArgumentOutOfRangeException(nameof(Fabricacion), "Año de fabricación inválido");
        Fabricacion = y;
    }

    void SetNumeroMotor(string v)
    {

        if (string.IsNullOrWhiteSpace(v))
            throw new ArgumentException("Numero de motor requerido", nameof(NumeroMotor));

        var trimmed = v.Trim();

        if (trimmed.Length != 17)
            throw new ArgumentException("El número de motor debe tener exactamente 17 caracteres.", nameof(NumeroMotor));

        if (!trimmed.All(char.IsLetterOrDigit))
            throw new ArgumentException("El número de motor debe ser alfanumérico.", nameof(NumeroMotor));

        NumeroMotor = trimmed;

    }

    void SetNumeroChasis(string v)
    {
        if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException("Numero de chasis requerido", nameof(NumeroChasis));
        NumeroChasis = v.Trim();
    }
}