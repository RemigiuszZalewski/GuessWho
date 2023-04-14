import API_ENDPOINT from "../config";
import axios from "axios";

const register = (username, password) => {
    return axios
        .post(API_ENDPOINT + "/Account/Register", {
            username,
            password,
        })
        .then((response) => {
            if (response.data.accessToken) {
                localStorage.setItem("user", JSON.stringify(response.data));
            }

            return response.data;
        });
};

const login = (username, password) => {
    return axios
        .post(API_ENDPOINT + "/Account/Login", {
            username,
            password,
        })
        .then((response) => {
            if (response.data.accessToken) {
                localStorage.setItem("user", JSON.stringify(response.data));
            }

            return response.data;
        });
};

const logout = () => {
    const user = getCurrentUser();
    if (user && user.refreshToken) {
        return axios
            .post(API_ENDPOINT + "/User/logout", { refreshToken: user.refreshToken })
            .then(() => {
                localStorage.removeItem("user");
            })
            .catch((error) => {
                console.error(error);
            });
    } else {
        localStorage.removeItem("user");
        return Promise.resolve();
    }
};

const getCurrentUser = () => {
    return JSON.parse(localStorage.getItem("user"));
};

const AuthService = {
    register,
    login,
    logout,
    getCurrentUser,
};

export default AuthService;