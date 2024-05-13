import GSAForm from "./GSAForm";
import GSAResults from "./GSAResults";
import styles from "../styles/gsaformcard.module.css";
import { useState } from "react";
import { GoalSeekCalcResponse } from "../entities/GoalSeekCalcResponse";

export default function GSAFormCard() {
  const [gsaResponse, setGsaResponse] = useState<GoalSeekCalcResponse>();

  return (
    <div className={styles.cardContainer}>
      <h2 className={styles.formHeading}>Goal Seek Algorithm Calculator</h2>
      <GSAForm setGsaResponse={setGsaResponse} />
      <GSAResults gsaResponse={gsaResponse} setGsaResponse={setGsaResponse} />
    </div>
  );
}
