import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import { HomePage } from "./pages/HomePage";
import { NavBar } from "./components/navigation/NavBar";
import { UserImagePage } from "./pages/UserImageDetailsPage";
import { CreateCommentPage } from "./pages/CreateCommentPage";
import { CreateUserImagePage } from "./pages/CreateUserImagePage";

function App() {
  return (
    <>
      <NavBar />
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/userimage/:id" element={<UserImagePage />} />
          <Route
            path="/createcomment/:userimageid"
            element={<CreateCommentPage />}
          />
          <Route path="/createuserimage" element={<CreateUserImagePage />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
