import { useEffect, useState } from "react";
import { getCryptoData } from "./api";

function App() {
    const [cryptoData, setCryptoData] = useState(null);

    useEffect(() => {
        getCryptoData().then(data => setCryptoData(data));
    }, []);




    
    return (
        <div>
            <h1>Crypto App</h1>
            {cryptoData ? <p>{cryptoData.message}</p> : <p>Loading...</p>}
        </div>
    );
}

export default App;
