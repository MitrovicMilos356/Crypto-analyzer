import React, { useState } from "react";

const CryptoHistoryForm = ({ fetchHistory }) => {
    const [symbol, setSymbol] = useState("BTC");
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();
        fetchHistory(symbol, startDate, endDate);
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Select Coin:
                <select value={symbol} onChange={(e) => setSymbol(e.target.value)}>
                    <option value="BTC">Bitcoin (BTC)</option>
                    <option value="ETH">Ethereum (ETH)</option>
                    <option value="DOGE">Dogecoin (DOGE)</option>
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