using QuantConnect.Data;
using QuantConnect.Indicators;

namespace QuantConnect.Algorithm.CSharp
{
    public class OBAMA : QCAlgorithm
    {
        private RelativeStrengthIndex _rsi;

        

        public override void Initialize()
        {
            // Initialisations...

            _rsi = RSI(_btcusd, 14, MovingAverageType.Simple, _resolution);

            // Autres initialisations...
        }

        public override void OnData(Slice data)
        {
            // Attendre que les indicateurs soient prêts
            if (this.IsWarmingUp || !_rsi.IsReady)
                return;

            // Logique de trading avec l'indicateur RSI
            if (!Portfolio.Invested && _rsi < 30)
            {
                // Conditions pour acheter
                SetHoldings(_btcusd, 1);
            }
            else if (Portfolio.Invested && _rsi > 70)
            {
                // Conditions pour vendre
                Liquidate(_btcusd);
            }
        }

        
    }
}
