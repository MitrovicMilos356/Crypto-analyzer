import React, { useState, useEffect } from "react";
import { getCryptoData } from "./api";

const CryptoTable = () => {
  const [coins, setCoins] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const data = await getCryptoData();
      setCoins(data);
    };
    fetchData();
  }, []);

  return (
    <div>
      <h2>Cryptocurrency Prices</h2>
      <table border="1">
        <thead>
          <tr>
            <th>Name</th>
            <th>Symbol</th>
            <th>Price (USD)</th>
          </tr>
        </thead>
        <tbody>
          {coins.map((coin) => (
            <tr key={coin.symbol}>
              <td>{coin.name}</td>
              <td>{coin.symbol}</td>
              <td>${coin.price.toFixed(7)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default CryptoTable;
