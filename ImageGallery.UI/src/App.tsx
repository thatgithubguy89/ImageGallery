import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import { HomePage } from "./pages/HomePage";
import { NavBar } from "./components/navigation/NavBar";
import { UserImagePage } from "./pages/UserImageDetailsPage";
import { CreateCommentPage } from "./pages/CreateCommentPage";
import { CreateUserImagePage } from "./pages/CreateUserImagePage";
import { SignInPage } from "./pages/SignInPage";
import { RegisterPage } from "./pages/RegisterPage";
import { ProfilePage } from "./pages/ProfilePage";

function App() {
  return (
    <>
      <BrowserRouter>
        <NavBar />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/userimage/:id" element={<UserImagePage />} />
          <Route
            path="/createcomment/:userimageid"
            element={<CreateCommentPage />}
          />
          <Route path="/createuserimage" element={<CreateUserImagePage />} />
          <Route path="/profile/:username" element={<ProfilePage />} />
          <Route path="/signin" element={<SignInPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="*" element={<HomePage />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
