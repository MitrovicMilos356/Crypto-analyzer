import React, { useState, useEffect } from "react";
import { getCryptoData } from "./api"; // Adjust the import based on your project structure

const CryptoTable = () => {
    const [coins, setCoins] = useState([]);
    const [sortedData, setSortedData] = useState([]);
    const [sortConfig, setSortConfig] = useState({ key: null, direction: "asc" });

    useEffect(() => {
        const fetchData = async () => {
            const data = await getCryptoData();
            setCoins(data);
            setSortedData(data); // Initialize sorted data
        };
        fetchData();
    }, []);

    const handleSort = (key) => {
        let direction = "asc";
        if (sortConfig.key === key && sortConfig.direction === "asc") {
            direction = "desc";
        }

        const sorted = [...sortedData].sort((a, b) => {
            if (a[key] < b[key]) return direction === "asc" ? -1 : 1;
            if (a[key] > b[key]) return direction === "asc" ? 1 : -1;
            return 0;
        });

        setSortedData(sorted);
        setSortConfig({ key, direction });
    };

    return (
        <div>
            <h2>Crypto Prices</h2>
            <table border="1">
                <thead>
                    <tr>
                        <th onClick={() => handleSort("name")}>
                            Name {sortConfig.key === "name" ? (sortConfig.direction === "asc" ? "↑" : "↓") : ""}
                        </th>
                        <th onClick={() => handleSort("symbol")}>
                            Symbol {sortConfig.key === "symbol" ? (sortConfig.direction === "asc" ? "↑" : "↓") : ""}
                        </th>
                        <th onClick={() => handleSort("price")}>
                            Price (USD) {sortConfig.key === "price" ? (sortConfig.direction === "asc" ? "↑" : "↓") : ""}
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {sortedData.map((coin) => (
                        <tr key={coin.symbol}>
                            <td>{coin.name}</td>
                            <td>{coin.symbol}</td>
                            <td>${coin.price.toFixed(2)}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default CryptoTable;
