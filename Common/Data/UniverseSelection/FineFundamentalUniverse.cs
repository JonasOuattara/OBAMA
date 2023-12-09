/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Linq;
using System.Collections.Generic;
using QuantConnect.Data.Fundamental;

namespace QuantConnect.Data.UniverseSelection
{
    /// <summary>
    /// Defines a universe that reads fine us equity data
    /// </summary>
    public class FineFundamentalUniverse : Universe
    {
        private readonly Func<IEnumerable<FineFundamental>, IEnumerable<Symbol>> _selector;

        /// <summary>
        /// Gets the settings used for subscriptions added for this universe
        /// </summary>
        public override UniverseSettings UniverseSettings { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FineFundamentalUniverse"/> class
        /// </summary>
        /// <param name="universeSettings">The settings used for new subscriptions generated by this universe</param>
        /// <param name="selector">Returns the symbols that should be included in the universe</param>
        public FineFundamentalUniverse(UniverseSettings universeSettings, Func<IEnumerable<FineFundamental>, IEnumerable<Symbol>> selector)
            : base(CreateConfiguration(FundamentalUniverse.SymbolFactory.UniverseSymbol()))
        {
            UniverseSettings = universeSettings;
            _selector = selector;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FineFundamentalUniverse"/> class
        /// </summary>
        /// <param name="symbol">Defines the symbol to use for this universe</param>
        /// <param name="universeSettings">The settings used for new subscriptions generated by this universe</param>
        /// <param name="selector">Returns the symbols that should be included in the universe</param>
        public FineFundamentalUniverse(Symbol symbol, UniverseSettings universeSettings, Func<IEnumerable<FineFundamental>, IEnumerable<Symbol>> selector)
            : base(CreateConfiguration(symbol))
        {
            UniverseSettings = universeSettings;
            _selector = selector;
        }

        /// <summary>
        /// Performs universe selection using the data specified
        /// </summary>
        /// <param name="utcTime">The current utc time</param>
        /// <param name="data">The symbols to remain in the universe</param>
        /// <returns>The data that passes the filter</returns>
        public override IEnumerable<Symbol> SelectSymbols(DateTime utcTime, BaseDataCollection data)
        {
            return _selector(data.Data.OfType<FineFundamental>());
        }

        /// <summary>
        /// Creates a <see cref="FineFundamental"/> subscription configuration for the US-equity market
        /// </summary>
        /// <param name="symbol">The symbol used in the returned configuration</param>
        /// <returns>A fine fundamental subscription configuration with the specified symbol</returns>
        public static SubscriptionDataConfig CreateConfiguration(Symbol symbol)
        {
            return FundamentalUniverse.CreateConfiguration(symbol);
        }
    }
}
