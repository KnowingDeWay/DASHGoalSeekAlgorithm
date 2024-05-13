import { useState } from "react";
import { GoalSeekCalcResponse } from "../entities/GoalSeekCalcResponse";
import styles from "../styles/gsaresults.module.css";

export default function GSAResults() {
  const [gsaResponse, setGsaResponse] = useState<GoalSeekCalcResponse>();

  return (
    <div className={styles.resultsContainer}>
      <h2>Target Result: {gsaResponse?.targetInput ?? "n/a"}</h2>
      <h2>
        Mininum Iterations Required: {gsaResponse?.iterationsRequired ?? "n/a"}
      </h2>
    </div>
  );
}
