import API from "./api";

const LOCAL_STORAGE_TOKEN_NAME = "authToken";

const GetPhotos = async () =>
  await API.get("photo", {
    headers: {
      Authorization: `Bearer ${localStorage.getItem(LOCAL_STORAGE_TOKEN_NAME)}`,
    },
  });

const AuthUser = async (LoginUser) =>
  await API.post("user/authenticate", LoginUser).then((respond) =>
    localStorage.setItem(LOCAL_STORAGE_TOKEN_NAME, respond.data.token)
  );

const GetCountOfPhotos = async () =>
  await API.get("photo/count", {
    headers: {
      Authorization: `Bearer ${localStorage.getItem(LOCAL_STORAGE_TOKEN_NAME)}`,
    },
  });

const PostUser = async (RegisterUser) =>
  await API.post("user", RegisterUser);

const IsTokenAvailable = () =>
  localStorage.getItem(LOCAL_STORAGE_TOKEN_NAME) !== null;

const Logout = () => localStorage.clear(LOCAL_STORAGE_TOKEN_NAME);

export {
  Logout,
  GetPhotos,
  AuthUser,
  GetCountOfPhotos,
  PostUser,
  IsTokenAvailable,
};