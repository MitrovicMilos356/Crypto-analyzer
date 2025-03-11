import axios from "axios";

const API_URL = "http://localhost:5071/api/crypto";

export const getCryptoData = async () => {
    try {
        const response = await axios.get(API_URL);
        return response.data;
    } catch (error) {
        console.error("Error fetching data:", error);
        return null;
    }
};
