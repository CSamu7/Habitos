import { Route, Switch } from "wouter";
import "./App.css";
import Login from "../features/auth/components/Login";
import MainPage from "../features/routine/components/MainPage";
import UserRegisterForm from "../features/register/components/UserRegisterForm";
import UserConfigurationForm from "../features/register/components/UserConfigurationForm";
import RoutinesPage from "../features/routine/components/RoutinesPage";

function App() {
  return (
    <Switch>
      <Route path="/login" component={Login}></Route>
      <Route path="/dashboard" component={MainPage}></Route>
      <Route path="/register" component={UserRegisterForm}></Route>
      <Route
        path="/account-configuration"
        component={UserConfigurationForm}
      ></Route>
      <Route path={"/my-routines"} component={RoutinesPage}></Route>
    </Switch>
  );
}

export default App;
