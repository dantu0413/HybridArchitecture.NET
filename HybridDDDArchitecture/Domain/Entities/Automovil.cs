namespace Domain.Entities
{
    public class Automovil
    {
        public int Id { get; private set; }
        public string Marca { get; private set; } = default!;
        public string Modelo { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public int Fabricacion { get; private set; }
        public string NumeroMotor { get; private set; } = default!;
        public string NumeroChasis { get; private set; } = default!;

        private Automovil() { } // EF

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

        public void Update(string? color, string? numeroMotor, string? numeroChasis)
        {
            if (!string.IsNullOrWhiteSpace(color)) SetColor(color);
            if (!string.IsNullOrWhiteSpace(numeroMotor)) SetNumeroMotor(numeroMotor);
            if (!string.IsNullOrWhiteSpace(numeroChasis)) SetNumeroChasis(numeroChasis);
        }

        void SetMarca(string v) { if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException(nameof(Marca)); Marca = v; }
        void SetModelo(string v) { if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException(nameof(Modelo)); Modelo = v; }
        void SetColor(string v) { if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException(nameof(Color)); Color = v; }
        void SetFabricacion(int y) { var now = DateTime.UtcNow.Year; if (y < 1900 || y > now) throw new ArgumentOutOfRangeException(nameof(Fabricacion)); Fabricacion = y; }
        void SetNumeroMotor(string v) { if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException(nameof(NumeroMotor)); NumeroMotor = v; }
        void SetNumeroChasis(string v) { if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException(nameof(NumeroChasis)); NumeroChasis = v; }
    }
}