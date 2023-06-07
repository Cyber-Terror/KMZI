namespace LAB_08
{
    public static class LinearCongruentialGenerator
    {
        private const int A = 430;
        private const int C = 2531;
        private const int N = 11979;
        private static int _clue = 1;

        public static int Clue
        {
            set
            {
                _clue = value;
            }
        }

        
        public static int Rand()
        {
            //x(t+1)=(a*xt+c)modN
            _clue = (A * _clue + C) % N;
            return _clue;
        }
    }
}
