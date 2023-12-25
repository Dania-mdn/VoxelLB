using BLINDED_AM_ME.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    //public WeaponHendler weaponHendler;

    public Material CapMaterial;

    private CancellationTokenSource _previousTaskCancel;

    public bool cut = false;

    private PlayerOptions PlayerOptions;
    public EnemyOptiuns enemyOptiuns;

    private float time; 
    public bool cuttime = false;

    private void Update()
    {
        if(time > 0)
        {
            time = time - Time.deltaTime;
        }
        else
        {
            cuttime = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!cut) return;

        var timeLimit = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;

        FindOptiuns(collision.collider.transform);

        if (collision.collider.gameObject.layer == 3)
        {
            return;
        }

        time = enemyOptiuns.coldawn;
        if (PlayerOptions != null)
        {
            if (PlayerOptions.TakeHit(enemyOptiuns.Damage))
            {
                if (collision.collider.GetComponent<MeshRenderer>() != null)
                {
                    Cutt(collision.collider.gameObject, timeLimit);
                }
            }
            else
            {
                EventSystem.DoAttackHealth(enemyOptiuns.Damage);
                cut = false;
            }
        }
        else
        {
            if (collision.collider.GetComponent<MeshRenderer>() != null)
            {
                Cutt(collision.collider.gameObject, timeLimit);
            }
        }
    }
    private void FindOptiuns(Transform collision)
    {
        while (collision != null)
        {
            if (collision.gameObject.GetComponent<PlayerOptions>() != null)
            {
                PlayerOptions = collision.GetComponent<PlayerOptions>();
                break;
            }

            collision = collision.parent;
        }
    }

    // �� ������� ���� ���������� �����������
    private void Cutt(GameObject target, CancellationToken cancellationToken = default)
    {
        try
        {
            _previousTaskCancel?.Cancel();
            _previousTaskCancel = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cancellationToken = _previousTaskCancel.Token;
            cancellationToken.ThrowIfCancellationRequested();

            // �������� ���� �����
            var leftSide = target;
            var leftMeshFilter = leftSide.GetComponent<MeshFilter>();
            var leftMeshRenderer = leftSide.GetComponent<MeshRenderer>();

            var materials = new List<Material>();
            leftMeshRenderer.GetSharedMaterials(materials);

            // �������
            var capSubmeshIndex = 0;
            if (materials.Contains(CapMaterial))
                capSubmeshIndex = materials.IndexOf(CapMaterial);
            else
            {
                capSubmeshIndex = materials.Count;
                materials.Add(CapMaterial);
            }

            // ���������� ���� ������� ������
            var blade = new Plane(
                leftSide.transform.InverseTransformDirection(transform.right),
                leftSide.transform.InverseTransformPoint(transform.position));

            var mesh = leftMeshFilter.sharedMesh;
            //var mesh = leftMeshFilter.mesh;

            // �������
            var pieces = mesh.Cut(blade, capSubmeshIndex, cancellationToken);

            leftSide.name = "LeftSide";
            leftMeshFilter.mesh = pieces.Item1;
            leftMeshRenderer.sharedMaterials = materials.ToArray();
            leftMeshRenderer.materials = materials.ToArray();

            var rightSide = new GameObject("RightSide");
            var rightMeshFilter = rightSide.AddComponent<MeshFilter>();
            var rightMeshRenderer = rightSide.AddComponent<MeshRenderer>();

            rightSide.transform.SetPositionAndRotation(leftSide.transform.position, leftSide.transform.rotation);
            rightSide.transform.localScale = leftSide.transform.localScale;

            rightMeshFilter.mesh = pieces.Item2;
            rightMeshRenderer.sharedMaterials = materials.ToArray();
            rightMeshRenderer.materials = materials.ToArray();

            // Գ���� 
            Destroy(leftSide.GetComponent<Collider>());

            // �������
            var leftCollider = leftSide.AddComponent<MeshCollider>();
            leftCollider.convex = true;
            leftCollider.sharedMesh = pieces.Item1;

            var rightCollider = rightSide.AddComponent<MeshCollider>();
            rightCollider.convex = true;
            rightCollider.sharedMesh = pieces.Item2;

            // rigidbody
            if (!leftSide.GetComponent<Rigidbody>())
                leftSide.AddComponent<Rigidbody>();

            if (!rightSide.GetComponent<Rigidbody>())
                rightSide.AddComponent<Rigidbody>();

            leftSide.transform.parent = null;

            leftSide.transform.DetachChildren();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    // �� �� ������� ���� ���������� �����������
    private IEnumerator CutCoroutine(GameObject target, CancellationToken cancellationToken = default)
    {
        _previousTaskCancel?.Cancel();
        _previousTaskCancel = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cancellationToken = _previousTaskCancel.Token;

        // �������� ���� �����
        var leftSide = target;
        var leftMeshFilter = leftSide.GetComponent<MeshFilter>();
        var leftMeshRenderer = leftSide.GetComponent<MeshRenderer>();

        var materials = new List<Material>();
        leftMeshRenderer.GetSharedMaterials(materials);

        // �������
        var capSubmeshIndex = 0;
        if (materials.Contains(CapMaterial))
            capSubmeshIndex = materials.IndexOf(CapMaterial);
        else
        {
            capSubmeshIndex = materials.Count;
            materials.Add(CapMaterial);
        }

        // ���������� ���� ������� ������
        var blade = new Plane(
            leftSide.transform.InverseTransformDirection(transform.right),
            leftSide.transform.InverseTransformPoint(transform.position));

        var mesh = leftMeshFilter.sharedMesh;
        //var mesh = leftMeshFilter.mesh;

        // �������
        yield return mesh.CutCoroutine(blade,
            (pieces) =>
            {
                leftSide.name = "LeftSide";
                leftMeshFilter.mesh = pieces.Item1;
                leftMeshRenderer.sharedMaterials = materials.ToArray();
                leftMeshRenderer.materials = materials.ToArray();

                var rightSide = new GameObject("RightSide");
                var rightMeshFilter = rightSide.AddComponent<MeshFilter>();
                var rightMeshRenderer = rightSide.AddComponent<MeshRenderer>();

                rightSide.transform.SetPositionAndRotation(leftSide.transform.position, leftSide.transform.rotation);
                rightSide.transform.localScale = leftSide.transform.localScale;

                rightMeshFilter.mesh = pieces.Item2;
                rightMeshRenderer.sharedMaterials = materials.ToArray();
                rightMeshRenderer.materials = materials.ToArray();

                // Physics 
                Destroy(leftSide.GetComponent<Collider>());

                // Replace
                var leftCollider = leftSide.AddComponent<MeshCollider>();
                leftCollider.convex = true;
                leftCollider.sharedMesh = pieces.Item1;

                var rightCollider = rightSide.AddComponent<MeshCollider>();
                rightCollider.convex = true;
                rightCollider.sharedMesh = pieces.Item2;

                // rigidbody
                if (!leftSide.GetComponent<Rigidbody>())
                    leftSide.AddComponent<Rigidbody>();

                if (!rightSide.GetComponent<Rigidbody>())
                    rightSide.AddComponent<Rigidbody>();

            }, capSubmeshIndex, cancellationToken);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        var top = transform.position + transform.up * 0.5f;
        var bottom = transform.position - transform.up * 0.5f;

        Gizmos.DrawRay(top, transform.forward * 5.0f);
        Gizmos.DrawRay(transform.position, transform.forward * 5.0f);
        Gizmos.DrawRay(bottom, transform.forward * 5.0f);
        Gizmos.DrawLine(top, bottom);
    }
}
