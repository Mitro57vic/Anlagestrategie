# Anlagestrategie

*Aufgabenstellung*

Projekt: Anlagestrategie 

Die Schüler entwickeln ein Programm, mit welchem man unterschiedliche Anlagestrategien kreieren kann für Investoren. Aufbauend auf historischen Renditen für gewisse Anlagen kann dadurch berechnet werden, mit welcher Anlageform gewisse Vermögensziele erreicht werden können. Dabei wird auch die Unsicherheit einer bestimmten Anlagestrategie berücksichtigt, indem die Varianz der Renditen dafür als Massstab genommen wird. 

Anforderungen 

1. Man kann das Anfangskapital eingeben und das gewünschte Vermögensziel zu einem bestimmten Zeitpunkt. 

2. Das Programm berechnet die Varianz (Mass für Unsicherheit) und die Korrelation zwischen zwei Anlagen (Mass für Grad der Diversifizierung der Anlagen). 

3. Die Applikation wählt dementsprechend verschiedene Strategien aus und wählt diejenige mit der grössten Sicherheit. 

Mögliche Erweiterungen 

· Anzahl der möglichen Anlagestrategien beliebig erweiterbar. 


Hier ist eine Beschreibung der drei Anlagestrategien, wie sie in diesem C#-Programm implementiert sind. Diese Erklärung ist als README.md-Text formatiert und kann direkt in ein GitHub-Repository eingefügt werden, um die Funktionsweise der Anlagestrategien und die Berechnungsmethoden klar zu erläutern.

---

# Anlagestrategien Erklärung

Dieses Projekt implementiert ein C#-Programm, mit dem verschiedene Anlagestrategien für ein Portfolio von Investitionen berechnet werden können. Das Programm umfasst drei Strategien:

1. **Standardstrategie**: Gewichtet nach historischen Renditen
2. **Growth-Strategie**: Gewichtet nach Wachstumsraten
3. **Value-Strategie**: Gewichtet nach Bewertung (Valuation)

Jede Strategie gewichtet die Anlagen unterschiedlich, basierend auf spezifischen Zielen und Finanzkennzahlen. Hier eine Übersicht über jede Strategie und wie die Gewichtungen berechnet werden.

---

## 1. Standardstrategie - Gewichtung nach durchschnittlichen historischen Renditen

Die Standardstrategie berechnet die Gewichtungen basierend auf den durchschnittlichen historischen Renditen jeder Anlage. Diese Methode geht davon aus, dass die historischen Renditen einen guten Indikator für zukünftige Erträge darstellen und sich das Portfolio optimal entwickelt, wenn die Kapitalverteilung proportional zur durchschnittlichen Rendite jeder Anlage erfolgt.

### Berechnung

Die Gewichtungen für diese Strategie werden folgendermaßen berechnet:

1. Berechne die **durchschnittliche historische Rendite** für jede Anlage.
2. Addiere die durchschnittlichen Renditen aller Anlagen, um eine Gesamtsumme der erwarteten Portfolio-Rendite zu erhalten.
3. Setze die Gewichtung jeder Anlage proportional zu ihrer durchschnittlichen Rendite im Verhältnis zur Gesamtrendite:




### Beispiel:

Angenommen, ein Portfolio enthält zwei Anlagen:

- **Anlage A**: durchschnittliche Rendite = 7 %
- **Anlage B**: durchschnittliche Rendite = 4 %

Die Summe der Renditen beträgt `7% + 4% = 11%`.

Die Gewichtungen wären:

- Anlage A: `7% / 11% = 63.64%`
- Anlage B: `4% / 11% = 36.36%`

---

## 2. Growth-Strategie - Gewichtung nach Wachstumsraten

Die Growth-Strategie gewichtet Anlagen nach ihrer erwarteten Wachstumsrate. Diese Strategie richtet sich an Anleger, die hohe Wachstumschancen in aufstrebenden Branchen suchen. Der Fokus liegt auf Unternehmen mit überdurchschnittlichen Wachstumsraten, oft unabhängig davon, ob sie derzeit hohe oder niedrige Gewinne erwirtschaften.

### Berechnung:

1. Bestimme die **Wachstumsrate** jeder Anlage (z. B. die durchschnittliche jährliche Wachstumsrate der Erträge).
2. Addiere die Wachstumsraten aller Anlagen zur Gesamtsumme.
3. Setze die Gewichtung jeder Anlage proportional zu ihrer Wachstumsrate im Verhältnis zur Gesamtsumme der Wachstumsraten:



---

## 2. Growth-Strategie - Gewichtung nach Wachstumsraten

Die Growth-Strategie gewichtet Anlagen nach ihrer erwarteten Wachstumsrate. Diese Strategie richtet sich an Anleger, die hohe Wachstumschancen in aufstrebenden Branchen suchen. Der Fokus liegt auf Unternehmen mit überdurchschnittlichen Wachstumsraten, oft unabhängig davon, ob sie derzeit hohe oder niedrige Gewinne erwirtschaften.

### Berechnung

Die Gewichtungen für die Growth-Strategie werden wie folgt berechnet:

1. Bestimme die **Wachstumsrate** jeder Anlage (z. B. die durchschnittliche jährliche Wachstumsrate der Erträge).
2. Addiere die Wachstumsraten aller Anlagen zur Gesamtsumme.
3. Setze die Gewichtung jeder Anlage proportional zu ihrer Wachstumsrate im Verhältnis zur Gesamtsumme der Wachstumsraten:


### Beispiel


### Beispiel:

Für das gleiche Portfolio:

- **Anlage A**: Wachstumsrate = 12 %
- **Anlage B**: Wachstumsrate = 8 %

Die Summe der Wachstumsraten beträgt `12% + 8% = 20%`.

Die Gewichtungen wären:

- Anlage A: `12% / 20% = 60%`
- Anlage B: `8% / 20% = 40%`


---

## 3. Value-Strategie - Gewichtung nach Bewertung (Valuation)

Die Value-Strategie basiert auf der Idee, dass Anlagen, die im Vergleich zu ihrem inneren Wert oder ihren fundamentalen Kennzahlen (wie Gewinn, Buchwert oder Cashflow) günstig bewertet sind, langfristig gute Renditen erzielen. Diese Strategie gewichtet daher Anlagen stärker, die eine niedrigere Bewertung aufweisen.

### Berechnung

Die Berechnung der Gewichtungen für die Value-Strategie erfolgt wie folgt:

1. Bestimme die **Bewertung** jeder Anlage, z. B. anhand des Kurs-Gewinn-Verhältnisses (KGV). Eine niedrige Bewertung bedeutet eine günstigere Anlage.
2. Berechne den **Kehrwert der Bewertung** jeder Anlage (d. h. 1 /Bewertung, um Anlagen mit niedrigeren Bewertungen höher zu gewichten.
3. Addiere die Kehrwerte aller Bewertungen zur Gesamtsumme.
4. Setze die Gewichtung jeder Anlage proportional zu ihrem Kehrwert der Bewertung im Verhältnis zur Gesamtsumme der Kehrwerte:



### Beispiel


### Beispiel:

Für das gleiche Portfolio:

- **Anlage A**: Bewertung (KGV) = 15
- **Anlage B**: Bewertung (KGV) = 10

Die Kehrwerte der Bewertungen sind:

- Anlage A: `1 / 15 = 0.0667`
- Anlage B: `1 / 10 = 0.1`

Die Summe der Kehrwerte beträgt `0.0667 + 0.1 = 0.1667`.

Die Gewichtungen wären:

- Anlage A: `0.0667 / 0.1667 = 40%`
- Anlage B: `0.1 / 0.1667 = 60%`

---

## Verwendung

Das Programm ermöglicht es dem Benutzer, eine der drei Anlagestrategien auszuwählen, und gibt die Gewichtungen für das Portfolio aus, die mit der gewählten Strategie berechnet wurden.

---

Diese Strategien ermöglichen unterschiedliche Ansätze für Investitionen und bieten Flexibilität je nach Risikobereitschaft und Investmentzielen. Durch eine klare Trennung der Methoden in `InvestmentStrategy`-Klassenmethoden kann die Anwendung leicht um zusätzliche Strategien erweitert werden.



 
