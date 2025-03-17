import React from "react";
import CryptoHistory from "./components/cryptoHistory";
import CryptoTable from "./components/CryptoTable";
import "./css/styles.css"; // Import the CSS

function App() {
    return (
      <div className="container">
        <div className="table-container">
          <CryptoTable />
        </div>
        <div className="chart-container">
          <CryptoHistory />
        </div>
      </div>
    );
  }

export default App;
