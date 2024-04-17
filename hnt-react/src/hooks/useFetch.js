import { useState, useEffect } from 'react';
import axios from 'axios';

function useFetch(url, options) {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            setIsLoading(true);
            try {
                const response = await axios.get(url, options);
                const result = await response.json();
                setData(result);
                setError(null);
            } catch(error) {
                setError(error);
            } finally {
                setIsLoading(false);
            }
        };
    }, [url, options]);
    return { data, error, isLoading };
}

export default useFetch;