dotnet ef migrations add InitialCreate_Write \
    -p Infrastructure \          # DbContext'in olduğu proje
    -s API \                     # Startup projesi
    --context WriteDbContext \    # Hedef DbContext
    --output-dir "Migrations/Write"  # Migration'ları ayrı klasöre koy