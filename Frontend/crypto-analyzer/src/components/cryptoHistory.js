import React, { useState } from "react";
import CryptoHistoryForm from "./CryptoHistoryForm";
import CryptoChart from "./CryptoChart";

const CryptoHistory = () => {
    const [historicalData, setHistoricalData] = useState([]);

    const fetchHistory = async (symbol, startDate, endDate) => {
        try {
            const response = await fetch(`http://localhost:5071/api/crypto/history?symbol=${symbol}&start=${startDate}&end=${endDate}`);
            const data = await response.json();
            setHistoricalData(data);
        } catch (error) {
            console.error("Error fetching history:", error);
        }
    };

    return (
        <div className="form-and-chart">
            <CryptoHistoryForm fetchHistory={fetchHistory} />
            <CryptoChart data={historicalData} />
        </div>
    );
};

export default CryptoHistory;
