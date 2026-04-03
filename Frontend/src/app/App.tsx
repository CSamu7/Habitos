import { Route, Switch } from "wouter";
import "./App.css";
import Login from "../features/auth/components/Login";

function App() {
  return (
    <Switch>
      <Route path="/login" component={Login}></Route>
    </Switch>
  );
}

export default App;
