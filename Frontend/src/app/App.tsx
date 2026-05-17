import { Route, Switch } from "wouter";
import "./App.css";
import Login from "../features/auth/components/Login";
import MainPage from "../features/routine/components/MainPage";
import RegisterPage from "../features/register/RegisterPage";

function App() {
  return (
    <Switch>
      <Route path="/login" component={Login}></Route>
      <Route path="/dashboard" component={MainPage}></Route>
      <Route path="/register" component={RegisterPage}></Route>
    </Switch>
  );
}

export default App;
