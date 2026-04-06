import { Route, Switch } from "wouter";
import "./App.css";
import Login from "../features/auth/components/Login";
import MainPage from "../features/routine/components/MainPage";

function App() {
  return (
    <Switch>
      <Route path="/login" component={Login}></Route>
      <Route path="/dashboard" component={MainPage}></Route>
    </Switch>
  );
}

export default App;
