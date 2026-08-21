import Header from "./Header";
import NavMenu from "./NavMenu";

export default function PublicNav() {
  return (
    <Header>
      <NavMenu>
        <li>
          <a href="/login">Iniciar sesión</a>
        </li>
      </NavMenu>
    </Header>
  );
}
