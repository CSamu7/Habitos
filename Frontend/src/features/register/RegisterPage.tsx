import { useState } from "react";
import styles from "./RegisterPage.module.css";
import PublicLayout from "../../Components/PublicLayout";
import RegisterStepOne from "./RegisterStepOne";
import RegisterStepTwo from "./RegisterStepTwo";
import { useForm } from "react-hook-form";

export interface UserRegisterForm {
  email: string;
  password: string;
  passwordAgain: string;
  username: string;
  timeZone: string;
  minGoal: number;
  cutOffTime: number;
}

export interface UserRegisterRequest {
  email: string;
  password: string;
  isPasswordCorrect: boolean | null;
  username: string;
  timeZone: string;
  minGoal: number;
  cutOffTime: number;
}

export default function RegisterPage() {
  const { register, handleSubmit, watch, formState } =
    useForm<UserRegisterForm>();

  const [steps, setSteps] = useState(1);

  const onContinueForm = () => {
    if (!formState.isValid) return;

    setSteps((s) => s + 1);
  };

  const onBackForm = () => {
    setSteps((s) => s - 1);
  };

  const onSendForm = () => {
    console.log("Enviado papulince");
  };

  return (
    <PublicLayout header="Creación de cuenta">
      {steps == 1 ? (
        <RegisterStepOne
          register={register}
          errors={formState.errors}
          onSubmit={handleSubmit}
          onContinueForm={onContinueForm}
        ></RegisterStepOne>
      ) : (
        <RegisterStepTwo
          onBackForm={onBackForm}
          onSendForm={onSendForm}
        ></RegisterStepTwo>
      )}
    </PublicLayout>
  );
}
