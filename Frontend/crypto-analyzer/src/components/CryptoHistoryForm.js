import React, { useState, useEffect } from "react";

const CryptoHistoryForm = ({ fetchHistory }) => {
    const [symbol, setSymbol] = useState("BTC");
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");
    const [coins, setCoins] = useState([]); // Store available coins

    // Fetch available coins when the component loads
    useEffect(() => {
        const fetchCoins = async () => {
            try {
                const response = await fetch("http://localhost:5071/api/crypto"); // Change to your API
                const data = await response.json();
                setCoins(data);
            } catch (error) {
                console.error("Error fetching coin list:", error);
            }
        };
        fetchCoins();
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        fetchHistory(symbol, startDate, endDate);
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Select Coin:
                <select value={symbol} onChange={(e) => setSymbol(e.target.value)}>
                    {coins.length > 0 ? (
                        coins.map((coin) => (
                            <option key={coin.symbol} value={coin.symbol}>
                                {coin.name} ({coin.symbol})
                            </option>
                        ))
                    ) : (
                        <option>Loading...</option>
                    )}
                </select>
            </label>
            <label>
                Start Date:
                <input type="date" value={startDate} onChange={(e) => setStartDate(e.target.value)} required />
            </label>
            <label>
                End Date:
                <input type="date" value={endDate} onChange={(e) => setEndDate(e.target.value)} required />
            </label>
            <button type="submit">Get History</button>
        </form>
    );
};

export default CryptoHistoryForm;
