import API from "./api"

const LOCAL_STORAGE_TOKEN_NAME = "authToken";

const GetPhotos = async () => await API.get('photo', {
        headers: {
            'Authorization': `Bearer ${localStorage.getItem(LOCAL_STORAGE_TOKEN_NAME)}`
        }
    });

const AuthUser = async ()  => await API.post('user/authenticate',{
        Login: "adam.nowak",
        Password: "aaaa"
    }).then(respond => localStorage.setItem(LOCAL_STORAGE_TOKEN_NAME, respond.data.token));


export {GetPhotos, AuthUser}