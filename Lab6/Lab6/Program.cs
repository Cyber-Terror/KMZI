namespace Lab6
{
    using System.Drawing;
    using System.Text;
    internal class Program
    {
        static void Main(string[] args)
        {
            var text = "A".ToUpper();
            //A - Y 

            var reflectorPairs = new Dictionary<char, char>()
                {
                    { 'A' , 'F'},
                    { 'B' , 'V'},
                    { 'C' , 'P'},
                    { 'D' , 'J'},
                    { 'E' , 'I'},
                    { 'G' , 'O'},
                    { 'H' , 'Y'},
                    { 'K' , 'R'},
                    { 'L' , 'Z'},
                    { 'M' , 'X'},
                    { 'N' , 'W'},
                    { 'T' , 'Q'},
                    { 'S' , 'U'},
                };

            // Создаем экземпляры классов Rotor и Reflector 

            Rotor rotor1 = new Rotor("ESOVPZJAYQUIRHXLNFTGKDCMWB".ToCharArray());
            Rotor rotor2 = new Rotor("FSOKANUERHMBTIYCWLQPZXVGJD".ToCharArray());
            Rotor rotor3 = new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE".ToCharArray());
            Reflector reflector = new Reflector(reflectorPairs);

            // Создаем экземпляр класса Enigma и устанавливаем начальные позиции роторов 
            Enigma enigma = new Enigma(rotor1, rotor2, rotor3, reflector);
            enigma.SetPositions("AAA"); // Устанавливаем начальные позиции роторов 

            var enigmaText = enigma.Encipher(text);

            Console.WriteLine(enigmaText);
        }
    }
    public class Enigma
    {
        private readonly Rotor rotor_1;
        private readonly Rotor rotor_2;
        private readonly Rotor rotor_3;
        private readonly Reflector _reflector;

        public Enigma(Rotor rotor1, Rotor rotor2, Rotor rotor3, Reflector reflector)
        {
            rotor_1 = rotor1;
            rotor_2 = rotor2;
            rotor_3 = rotor3;
            _reflector = reflector;
        }

        public string Encipher(string input)
        {
            string output = "";
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    output += Encipher(char.ToUpper(c));
                }
                else
                {
                    output += c;
                }
            }
            return output;
        }

        public char Encipher(char input)
        {
            {
                //// Поворачиваем роторы перед шифрованием символа 
                //if (rotor_1.GetPosition() == 'R' || rotor_2.GetPosition() == 'F') 
                //{ 
                //    rotor_2.Rotate(); 
                //} 
                //if (rotor_2.GetPosition() == 'W') 
                //{ 
                //    rotor_3.Rotate(); 
                //} 
                //rotor_1.Rotate(); 
            }
            // Проходим символ через 3 ротора и рефлектор 
            char output = rotor_1.Encipher(input);
            output = rotor_2.Encipher(output);
            output = rotor_3.Encipher(output);
            output = _reflector.Reflect(output);
            output = rotor_3.Encipher(output, true);
            output = rotor_2.Encipher(output, true);
            output = rotor_1.Encipher(output, true);

            // Возвращаем шифрованный символ 
            return output;
        }

      

        public void SetPositions(string positions)
        {
            if (positions.Length != 3)
            {
                throw new ArgumentException("Positions must be a string of length 3");
            }
            rotor_1.SetPosition(positions[0]);
            rotor_2.SetPosition(positions[1]);
            rotor_3.SetPosition(positions[2]);
        }
    }
    public class Rotor
    {
        private readonly char[] _wiring;
        private readonly char[] _inverseWiring;
        private int _position;

        public Rotor(char[] wiring)
        {
            _wiring = wiring;
            _inverseWiring = new char[wiring.Length];
            for (int i = 0; i < wiring.Length; i++)
            {
                _inverseWiring[wiring[i] - 'A'] = (char)('A' + i);
            }
            _position = 0;
        }

        public char Encipher(char input, bool reverse = false)
        {
            int index = (input - 'A' + _position) % 26;
            char output = reverse ? _inverseWiring[index] : _wiring[index];
            return (char)('A' + (output - 'A' - _position + 26) % 26);
        }

        public void Rotate()
        {
            _position = (_position + 1) % 26;
        }

        public void SetPosition(char position)
        {
            _position = position - 'A';
        }

        public char GetPosition()
        {
            return (char)('A' + _position);
        }
    }
    public class Reflector
    {
        private readonly Dictionary<char, char> _wiring;

        public Reflector(Dictionary<char, char> wiring)
        {
            _wiring = wiring;
        }

        public char Reflect(char input)
        {
            char value;
            if (_wiring.ContainsKey(input))
            {
                value = _wiring[input];
            }
            else
            {
                value = _wiring.First(x => x.Value == input).Key;
            }
            return value;
        }
    }
}