import {HTTPClient} from "./api";

const IAAPI = {
    async perguntarAsync(pergunta, usuarioId) {
        try {
            const response = await HTTPClient.post(
                `/IA/Perguntar?usuarioId=${usuarioId}`,
                JSON.stringify(pergunta),
                { headers: { "Content-Type": "application/json" } }
            );
            return response.data;
        } catch (error) {
            console.error("Erro ao perguntar:", error);
            throw error;
        }
    }
};

export default IAAPI;
