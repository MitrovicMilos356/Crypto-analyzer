import axios from "axios";
import React, { useState } from "react";
import CryptoHistoryForm from "./CryptoHistoryForm";
import CryptoChart from "./CryptoChart";

const API_URL = "http://localhost:5071/api/crypto";

export const getCryptoData = async () => {
    try {
        const response = await axios.get(API_URL);
        console.log(response);
        return response.data;
    } catch (error) {
        console.error("Error fetching data:", error);
        return null;
    }
};



