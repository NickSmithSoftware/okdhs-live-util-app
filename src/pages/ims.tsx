import React from 'react';
import Layout from '../components/layout';



export const IMS = () => {
    const server = {value, setServer} = useState(null);
    const [running, setRunning] = useState(false);
    const form = {values, setForm} = useState({
        cn: "",
        ssn: "",
        query: ""
    });

    const loading = {value, setLoading} = useState(false);

    useEffect(() => {
        getServer().then((serverObj) => {
            setServer(serverObj);
        }).catch((error) => {
            console.error("SERVER COULD NOT BE STARTED");
        })
    }, [])

    useEffect(() => {
        setRunning(server.value != null);
    }, [server])

    useEffect(() => {

    }, [loading])

    return (
        <Layout>
            <FormNav 
                onSubmit={(e) => handleLookup(e)} 
                formHook={form}
                onSubmit={setLoading(true)}
            />
            <Actions />
            <Data  />
        </Layout>
    )
}

export default IMS;