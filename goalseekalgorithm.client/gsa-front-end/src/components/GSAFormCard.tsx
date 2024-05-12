import { useState } from "react";
import styles from "../styles/gsaformcard.module.css";

export default function GSAFormCard() {
  const [formula, setFormula] = useState<string>("");
  const [initVal, setInitVal] = useState<string>("");
  const [targetResult, setTargetResult] = useState<string>("");

  return (
    <div className={styles.cardContainer}>
      <h2 className={styles.formHeading}>Goal Seek Algorithm Calculator</h2>
      <div className={styles.formContainer}>
        <form>
          <table>
            <tr>
              <td>
                <div className={styles.inputCell}>
                  <label htmlFor="formulaInput">
                    Enter Formula (use 'input' as variable)
                  </label>
                  <br />
                  <input
                    type="text"
                    id="formulaInput"
                    className={styles.inputField}
                    value={formula}
                    onChange={(e) => setFormula(e.target.value)}
                  />
                </div>
              </td>
              <td>
                <div className={styles.inputCell}>
                  <label htmlFor="initialInput">Enter Initial Value (x0)</label>
                  <br />
                  <input
                    type="text"
                    id="initialInput"
                    className={styles.inputField}
                    value={initVal}
                    onChange={(e) => setInitVal(e.target.value)}
                  />
                </div>
              </td>
            </tr>
            <tr>
              <td>
                <div className={styles.inputCell}>
                  <label htmlFor="targetResInput">Enter Target Result</label>
                  <br />
                  <input
                    type="text"
                    id="targetResInput"
                    className={styles.inputField}
                    value={targetResult}
                    onChange={(e) => setTargetResult(e.target.value)}
                  />
                </div>
              </td>
              <td></td>
            </tr>
          </table>
        </form>
      </div>
    </div>
  );
}
